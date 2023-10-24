import { User } from "../../objects/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface IUserHttpRepository extends IBaseHttpRepository<User> {
  signinAsync(email: string, password: string): Promise<User | undefined>;
}
