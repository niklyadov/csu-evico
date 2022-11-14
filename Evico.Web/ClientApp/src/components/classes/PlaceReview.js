export class PlaceReview{
    /**
     * @type {number}
     */
    id = null;

    /**
     * @type {boolean}
     */
    isDeleted = null;

    /**
     * @type {Date?}
     */
    deletedDateTime = null;

    /**
     * @type {number?}
     */
    placeId = null;

    /**
     * @type {{}}
     */
    author = null;

    /**
     * @type {number?}
     */
    authorId = null;

    /**
     * @type {string?}
     */
    comment = null;

    /**
     * @type {number?}
     */
    rate = null;

    /**
     * @type {[{}]}
     */
    photos = null;

    /** @param {PlaceReview} args */
    constructor(args) {

        Object.keys(this).forEach(k => args[k] !== undefined ? this[k] = args[k] : 0);

    };
}