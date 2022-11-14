export class PlaceCategory{

    /**
     * @type {string?}
     */
    name = null;

    /**
     * @type {string?}
     */
    description = null;

    /**
     * @type {number?}
     */
    parentId = null;

    /**
     * @type {[{}]}
     */
    places = [];

    /** @param {PlaceCategory} args */
    constructor(args) {

        Object.keys(this).forEach(k => args[k] !== undefined ? this[k] = args[k] : 0);

    };
}