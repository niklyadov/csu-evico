import { Place } from "./Place";

export class Event {

    /**
     * Дата завершения.
     * @type {Date?}
    */
    end = null;
    /**
     * Дата начала.
     * @type {Date?}
    */
    start = null;
    /**
     * Место.
     * @type {Place?}
    */
    place = null;
    /**
     * Идентификатор места.
     * @type {number?}
    */
    placeId = null;
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
     * Идентификатор фотографии.
     * @type {number?}
    */
    photoId = null;
    /**
     * Организаторы.
     * @type {[{}]}
    */
    organizers = [];
    /**
     * Участники.
     * @type {[{}]}
    */
    participants = [];
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
    locationLatitude = null;
    /**
     * Координата Y.
     * @type {number?}
    */
    locationLongitude = null;
    /**
     * Время удаления записи.
     * @type {Date}
    */
    deletedDateTime = null;

    /** @param {Event} args */
    constructor(args) {

        Object.keys(this).forEach(k => args[k] !== undefined ? this[k] = args[k] : 0);

    };

};