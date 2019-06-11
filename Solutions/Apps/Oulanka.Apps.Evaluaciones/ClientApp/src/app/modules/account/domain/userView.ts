export class UserView {

    Id: number;
    Username: string;
    Email: string;

    constructor(id: number, username: string, email: string) {
        this.Id = id;
        this.Username = username;
        this.Email = email;
    }
}