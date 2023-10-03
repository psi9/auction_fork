import { User } from "../../domain/Entities"
import IBaseHttpRepository from "./IBaseHttpRepository"

export default interface IUserHttpRepository extends IBaseHttpRepository<User> {
    
}