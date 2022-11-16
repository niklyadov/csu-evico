export class Place {

    /**
     * Идентификатор.
     * @type {number}
    */
    id = null;
    /**
     * Наименование.
     * @type {string?}
    */
    name = null;
    /**
     * Фото.
     * @type {string?}
    */
    photo = null;
    /**
     * Фотографии.
     * @type {[string]}
    */
    photos = [];
    /**
     * Отзывы.
     * @type {[string]}
    */
    reviews = [];
    /**
     * Метка удалено.
     * @type {boolean}
    */
    isDeleted = false;
    /**
     * Категории.
     * @type {[string]}
    */
    categories = [];
    /**
     * Описание.
     * @type {string?}
    */
    description = null;
    /**
     * Координата X.
     * @type {number?}
    */
    locationLatitude = 0;
    /**
     * Координата Y.
     * @type {number?}
    */
    locationLongitude = 0;
    /**
     * Время удаления записи.
     * @type {Date}
    */
    deletedDateTime = null;

    /** @param {Place} args */
    constructor(args) {
    
        Object.keys(this).forEach(k => args[k] !== undefined ? this[k] = args[k] : 0);

    };

};