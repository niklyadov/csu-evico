import { Place } from "../../components/classes/Place";
import config from "../../config";
import { errorHanlde } from "./errors";

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjcyZWM1YjVkLTBkOGItNDJiOS05ZjI1LTBkYWFlNmE5YWY5MyIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhSG90RG9nIiwiZW1haWwiOiIiLCJqdGkiOiIyMjk1NWUyOC1lNWExLTRkYmEtOTdkMi05ZjQ3OWRhZTIwZmIiLCJuYmYiOjE2Njg1ODg5MTcsImV4cCI6MTY2OTE5MzcxNywiaWF0IjoxNjY4NTg4OTE3LCJpc3MiOiJDU1UtRVZJQ08iLCJhdWQiOiJDU1UtRVZJQ08ifQ.7zFDS1E57J96T6nIHUcRNnc73IS-RpLYl9JwaopLYtECuHUgnehH9P7s52sn6hvrYUPuDPR1KvThq3r5yvWvvg";

/**
 * 
 * @param {Place} placeRecord 
 */
export const createPlace = function (placeRecord) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(placeRecord)
        })
        .then(response => { console.log(response); response.json(); })
        .then(data => {
            console.log(data);
            resolve(data);
        })
        .catch(errorHanlde);
    });
}


export const getPlacesList = function () {
    return new Promise(async (resolve, reject) => {
        let placesList = [];
        return fetch(`${config.api}place`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            for(const place of data){
                let placeObj = new Place(place);
                placesList.push(placeObj);
            }
            resolve(placesList);
        })
        .catch(errorHanlde);
    });
}


export const getPlaceById = function (placeId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let placeObj = new Place(data);
            resolve(placeObj);
        })
        .catch(errorHanlde);
    });
}

/**
 * 
 * @param {Place} changedPlaceRecord 
 */
export const changePlace = function (changedPlaceRecord) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place`, {
            method: "PUT",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(changedPlaceRecord)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const deletePlaceById = function (placeId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}`, {
            method: "DELETE",
            mode: "cors",
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


export const getUserPlacesList = function () {
    return new Promise (async (resolve, reject) => {
        let placesList = [];
        return fetch(`${config.api}place/my`, {
            method: "GET",
            mode: "cors",
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            for (const place of data){
                let placeObj = new Place(place);
                placesList.push(placeObj);
            }
            resolve(placesList);
        })
        .catch(errorHanlde);
    })
}


export const getUserPlaceById = function (placeId) {
    return new Promise (async (resolve, reject) => {
        return fetch(`${config.api}place/my/${placeId}`, {
            method: "GET",
            mode: "cors",
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let placeObj = new Place(data);
            resolve(placeObj);
        })
        .catch(errorHanlde);
    })
}