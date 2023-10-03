export default interface IBaseHttpRepository<T> {
    getAsync(): T[],

    postAsync(entity: T): void,

    putAsync(entity: T): void,

    deleteAsync(id: number): void
}