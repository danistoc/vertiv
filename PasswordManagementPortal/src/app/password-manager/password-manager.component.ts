import { Component, OnInit } from '@angular/core';
import { User, UserViewModel } from '../infrastructure/user';
import { PasswordManagementServiceService } from '../password-management-service.service';
import { ToastrService } from 'ngx-toastr';
import { formatDate } from "@angular/common";

@Component({
  selector: 'app-password-manager',
  templateUrl: './password-manager.component.html',
  styleUrls: ['./password-manager.component.css']
})
export class PasswordManagerComponent implements OnInit {

  users: UserViewModel[] = [];
  displayedColumns: string[] = ['id', 'username', 'password', 'expiration', 'isValid', 'timeRemaining', 'actions'];

  constructor(private service: PasswordManagementServiceService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.service.listUsers().subscribe(users => {
      this.updateUsersViewModels(users);
    });
  }

  private updateUsersViewModels(users: User[]) {
    this.users = users.map(u => {
      return this.mapUser(u);
    });
  }

  private mapUser(u: User): UserViewModel {
    var passwordExpirationDate:Date = new Date(u.passwordExpirationDate)
    var now:Date = new Date();
    var isValid = passwordExpirationDate > now;
    var timeRemainingInSeconds:number =  isValid ? Math.floor((passwordExpirationDate.getTime() - now.getTime() )/1000) : 0;
    return {
      id: u.id,
      username: u.username,
      password: u.password,
      passwordExpirationDate: passwordExpirationDate.toISOString(),
      isValid: isValid,
      secondsRemaining: timeRemainingInSeconds
    };
  }

  onGeneratePasswordClick(user: User): void {
    var format = "yyyy-MM-dd'T'HH:mm:ss";
    var creationDate = formatDate(Date.now(), format, "en");
    this.service.generateUserPassword(user.id, creationDate).subscribe(rs => {
      this.toastr.success(`New password:${rs.password} generated for ${user.username}`);
      this.loadUsers();
    });
  }
}
