import { User } from "../../objects/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface IUserHttpRepository
  extends IBaseHttpRepository<User> {}
