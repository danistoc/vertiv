import { Injectable } from '@angular/core';
import { User } from './infrastructure/user';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class PasswordManagementServiceService {

  private baseUrl:string = "https://localhost:44379";
  private listUsersUrl = `${this.baseUrl}/api/PasswordManagement/list`;
  private generatePasswordUrl = `${this.baseUrl}/api/PasswordManagement/generate`;

  constructor(private http: HttpClient, private toastr:ToastrService) { }

  listUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.listUsersUrl);
  }

  generateUserPassword(userId: number, creationDate: string): Observable<any> {
    var rq = { UserId: userId, CreationDate: creationDate };
    return this.http.post<any>(this.generatePasswordUrl, rq);
  }
}
