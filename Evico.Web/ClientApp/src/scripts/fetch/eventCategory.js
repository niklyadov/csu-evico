import { EventCategory } from "../../components/classes/EventCategory";
import config from "../../config";
import { errorHanlde } from "./errors";

const token = config.bearerToken;

/**
 * 
 * @param {EventCategory} eventCategory 
 */
export const createEventCategory = function (eventCategory) {
    return new Promise(async (resolve, reject) => {
        return fetch (`${config.api}eventcategory`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(eventCategory)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const getEventCategoriesList = function () {
    return new Promise(async (resolve, reject) => {
        let categoriesList = [];
        return fetch(`${config.api}eventcategory`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            for (const category of data){
                let categoryObj = new EventCategory(category);
                categoriesList.push(categoryObj);
            }
            resolve(categoriesList);
        })
        .catch(errorHanlde);
    });
}


export const getEventCategoryById = function (categoryId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}eventcategory/${categoryId}`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let categoryObj = new EventCategory(data);
            resolve(categoryObj);
        })
        .catch(errorHanlde);
    });
}

/**
 * 
 * @param {EventCategory} changedEventCategory 
 */
export const changeEventCategory = function (changedEventCategory) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}eventcategory`, {
            method: "PUT",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify(changedEventCategory)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const deleteEventCategoryById = function (categoryId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}eventcategory/${categoryId}`, {
            method: "DELETE",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}