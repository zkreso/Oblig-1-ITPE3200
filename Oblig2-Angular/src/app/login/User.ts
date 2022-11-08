export class User {
  Id: number;
  Username: string;
  Password: string;

  constructor(Id: number, Username: string, Password: string) {
    this.Id = Id;
    this.Username = Username;
    this.Password = Password;
  }
}
