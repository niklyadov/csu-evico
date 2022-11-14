import { PlaceCategory } from "../../components/classes/PlaceCategory";
import config from "../../config"

const token = "token";
const placeCategory = {
    "name": "name",
    "description": "desc",
}


export const createPlaceCategory = function () {
    return new Promise(async (resolve, reject) => {
        return fetch (`${config.api}placecategory`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(placeCategory)
        })
        .then(response => response.json())
        .then(data => resolve(data));
    });
}


export const getPlaceCategoriesList = function () {
    return new Promise(async (resolve, reject) => {
        let categoriesList = [];
        return fetch(`${config.api}placecategory`, {
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
                let categoryObj = new PlaceCategory(category);
                categoriesList.push(categoryObj);
            }
            resolve(categoriesList);
        });
    });
}


export const getPlaceCategoryById = function (categoryId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}placecategory/${categoryId}`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let categoryObj = new PlaceCategory(data);
            resolve(categoryObj);
        });
    });
}


export const changePlaceCategory = function () {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}placecategory`, {
            method: "PUT",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => resolve(data));
    });
}


export const deletePlaceCategoryById = function (categoryId) {
    return new Promise(async (resolve, reject) => {
        return fetch (`${config.api}placecategory/${categoryId}`, {
            method: "DELETE",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => resolve(data));
    });
}
