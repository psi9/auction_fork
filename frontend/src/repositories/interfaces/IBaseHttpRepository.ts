export default interface IBaseHttpRepository<T> {
    getAsync(): Promise<T[]>,

    postAsync(entity: T): Promise<void>,

    putAsync(entity: T): Promise<void>,

    deleteAsync(id: number): Promise<void>
}