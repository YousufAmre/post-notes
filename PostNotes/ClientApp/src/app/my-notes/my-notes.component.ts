import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm } from '@angular/forms';
import { initialNoteValue, Note } from '../models/Note';
import { MynotesService } from './mynotes.service';

@Component({
  selector: 'app-my-notes',
  templateUrl: './my-notes.component.html',
  styleUrls: ['./my-notes.component.css']
})
export class MyNotesComponent implements OnInit {

  notes: Note[];
  note: Note;
  isCreating: boolean = false;
  isDeleted: boolean = false;
  isEdited: boolean = false;
  notesForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
  });

  constructor(private _mynotesService: MynotesService, private datePipe: DatePipe) { }

  ngOnInit() {
    this.note = initialNoteValue;
    this.isCreating = false;
    this.getNotes();

  }

  getNotes() {
    this._mynotesService.getNotes().subscribe(
      response => {
        this.notes = response;
      },
      responseError => {
        this.notes = null;
        console.log(responseError);
      }
    );
  }

  getNoteById(id) {
    this._mynotesService.getNoteById(id).subscribe(
      responseSuccess => {
        this.note = responseSuccess;
        console.log(responseSuccess);
      },
      responseError => {
        this.note = null;
        console.log(responseError);
      }
    );
  }

  createNote() {
    this.isCreating = true;
    this.note = this.notesForm.value;
    this._mynotesService.postNote(this.note).subscribe(
      response => {
        if (response != null) {
          this.isCreating = false;
          this.initializeForm();
          this.ngOnInit();
        }
        else {
          this.isCreating = false;
        }
      }
    );
  }

  updateANote(noteId) {
    var noteToUpdate;
    if (this.notesForm.value.title == '') {
      noteToUpdate = { title: this.note.title, description: this.notesForm.value.description }
    }
    else if (this.notesForm.value.description == '') {
      noteToUpdate = { title: this.notesForm.value.title, description: this.note.description }
    }
    else {
      noteToUpdate = { title: this.notesForm.value.title, description: this.notesForm.value.description }
    }
    this._mynotesService.updateNote(noteId, noteToUpdate).subscribe(
      response => {
        if (response) {
          this.isEdited = true;
          this.getNotes();
          this.initializeForm();
          this.closeModel();
        }
      }

    );

    
  }

  deleteNote(id) {
    this._mynotesService.deleteNote(id).subscribe(
      response => {
        if (response != null) {
          this.isDeleted = true;
          this.ngOnInit();
        }
      }
    );
  }

  formatDate(value) {
    const date = new Date(value);
    return this.datePipe.transform(date, 'dd/MM/yyyy');
  }

  initializeForm() {
    this.notesForm = new FormGroup({
      title: new FormControl(''),
      description: new FormControl(''),
    });
  }

  openEditModal(id) {
    // Get the modal
    var modal = document.getElementById("editModal");

    // When the user clicks on the button, open the modal
    modal.style.display = "block";

    this.getNoteById(id);

    
  }

  closeModel() {
    var modal = document.getElementById("editModal");

    // When the user clicks on the button, open the modal
    modal.style.display = "none";
  }

}
