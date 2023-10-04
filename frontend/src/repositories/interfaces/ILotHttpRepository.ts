import { Lot } from "../../domain/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface ILotHttpRepository
  extends IBaseHttpRepository<Lot> {}