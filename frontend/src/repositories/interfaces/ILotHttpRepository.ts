import { Lot } from "../../objects/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface ILotHttpRepository extends IBaseHttpRepository<Lot> {
  postFormDataAsync(formData: FormData): Promise<boolean>;
}
