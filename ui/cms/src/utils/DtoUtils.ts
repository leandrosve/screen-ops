export default class DtoUtils {
  private static deepEqual(obj1: any, obj2: any): boolean {
    if (obj1 === obj2) return true;
    if (
      typeof obj1 !== "object" ||
      obj1 === null ||
      typeof obj2 !== "object" ||
      obj2 === null
    )
      return false;

    const keys1 = Object.keys(obj1);
    const keys2 = Object.keys(obj2);

    if (keys1.length !== keys2.length) return false;

    for (let key of keys1) {
      if (!keys2.includes(key) || !DtoUtils.deepEqual(obj1[key], obj2[key])) {
        return false;
      }
    }

    return true;
  }

  public static getModifiedFields<T extends object>(
    original: T,
    edited: T
  ): Partial<T> {
    const modifiedFields: Partial<T> = {};

    for (const key in original) {
      if (original.hasOwnProperty(key)) {
        if (!DtoUtils.deepEqual(original[key], edited[key])) {
          modifiedFields[key] = edited[key];
        }
      }
    }

    return modifiedFields;
  }
}
