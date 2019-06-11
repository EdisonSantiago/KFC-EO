export class OperationResult {
    Succeeded: boolean;
    Message: string;
    Value: any;

    constructor(succeeded: boolean, message: string) {
        this.Succeeded = succeeded;
        this.Message = message;
    }

}
