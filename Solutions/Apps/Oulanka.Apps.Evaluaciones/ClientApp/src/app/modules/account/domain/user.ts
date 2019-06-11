export class User {
  Username: string;
  Password: string;
  RememberMe: boolean;
  Token: any;
  ExpiresIn: number;

  constructor(username: string, password: string) {
    this.Username = username;
    this.Password = password;
    this.RememberMe = false;
    this.Token = '';
    this.ExpiresIn = 0;
  }
}
