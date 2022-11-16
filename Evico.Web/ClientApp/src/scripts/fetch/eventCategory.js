import { EventCategory } from "../../components/classes/EventCategory";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImY1YmJjYmM5LWJlYjctNDBlZS05YzdjLTE3YzY2Mjg3OWVmOCIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiMjhiNzBjNTgtYWQxNy00OWNlLWJmOTEtOGVhM2JkZDFjY2UzIiwibmJmIjoxNjY4NDEzNTY3LCJleHAiOjE2NjkwMTgzNjcsImlhdCI6MTY2ODQxMzU2NywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.pPV-ojMNwAIyo4SB3tlja002xYvMV4JUEInhwwagHhmu_e1TxBeSeGlvxxn_dk0UQoNHNn6B9BnSm_22GstQOA";

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
        .catch(error => reject(error));
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
        .catch(error => reject(error));
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
        .catch(error => reject(error));
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
        .catch(error => reject(error));
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
        .catch(error => reject(error));
    });
}