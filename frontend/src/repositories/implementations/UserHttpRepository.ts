import { User } from "../../domain/Entities";
import IUserHttpRepository from "../interfaces/IUserHttpRepository";

export default class UserHttpRepository implements IUserHttpRepository {
  private baseURL: string;

  constructor(baseURL: string) {
    this.baseURL = baseURL;
  }

  getAsync(): User[] {
    throw new Error("Method not implemented.");
  }

  postAsync(entity: User): void {
    throw new Error("Method not implemented.");
  }

  putAsync(entity: User): void {
    throw new Error("Method not implemented.");
  }
  
  deleteAsync(id: number): void {
    throw new Error("Method not implemented.");
  }
}
