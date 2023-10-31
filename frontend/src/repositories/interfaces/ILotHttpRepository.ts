import { Lot } from "../../objects/Entities";
import IBaseHttpRepository from "./IBaseHttpRepository";

export default interface ILotHttpRepository extends IBaseHttpRepository<Lot> {
  getByAuctionAsync(auctionId: string): Promise<Lot[] | undefined>;

  createLotAsync(formData: FormData): Promise<void>;
}
