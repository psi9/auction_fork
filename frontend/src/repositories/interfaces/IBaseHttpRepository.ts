import { Result } from "../../objects/Entities";

export default interface IBaseHttpRepository<T> {
  getAsync(): Promise<Result<T>>;

  postAsync(entity: T): Promise<boolean>;

  putAsync(entity: T): Promise<boolean>;

  deleteAsync(id: number): Promise<boolean>;
}
