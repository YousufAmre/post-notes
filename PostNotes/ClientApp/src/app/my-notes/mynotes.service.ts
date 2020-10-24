import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Note } from '../models/Note';

@Injectable({
  providedIn: 'root'
})
export class MynotesService {

  notes: Note[]
  constructor(private http: HttpClient) { }

  getNotes(): Observable<Note[]> {
    return this.http.get<Note[]>('https://localhost:5001/api/notes').pipe(catchError(this.errorHandler));
  }
  getNoteById(id): Observable<Note> {
    return this.http.get<Note>('https://localhost:5001/api/notes/' + id).pipe(catchError(this.errorHandler));
  }
  postNote(note: Note): Observable<Note> {
    const obj = { title: note.title, description: note.description }
    return this.http.post<Note>('https://localhost:5001/api/notes', obj).pipe(catchError(this.errorHandler));
  }
  updateNote(id, note): Observable<boolean>{
    return this.http.put<boolean>('https://localhost:5001/api/notes/' + id, note).pipe(catchError(this.errorHandler));
  }
  deleteNote(id): Observable<Note> {
    return this.http.delete<Note>('https://localhost:5001/api/notes/' + id).pipe(catchError(this.errorHandler));
  }
  errorHandler(error: HttpErrorResponse) {
    console.error(error);
    return throwError(error.message || "Server Error");
  }
}
