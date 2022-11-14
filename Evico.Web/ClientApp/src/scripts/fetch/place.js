import { Place } from "../../components/classes/Place";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjUyZWVlYWNkLWNiOTMtNGE4Yy05ZjFiLTRmNTBlNTVjZmU3MyIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiOWMzNjZjNTctZTlhNC00ODg0LTkwMDUtYWY5MDAwNDE4Mzc5IiwibmJmIjoxNjY4MTU2MzM3LCJleHAiOjE2Njg3NjExMzcsImlhdCI6MTY2ODE1NjMzNywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.VEAw-QDPRCIQcdqVnRIXmyXrlfm_RE4EYxw-X2dVeomNL7EBDAV5Kn7SzfpZOkDat9Ho1uHfdNkSGm06ZXq_4w";
//const placeRecord = {"locationLatitude": 0, "locationLongitude": 0, "name": "string", "description": "string"};
//const changedPlaceRecord = {"id": 1, "locationLatitude": 1, "locationLongitude": 1, "name": "string123", "description": "string123"};


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
        .then(response => response.json())
        .then(data => resolve(data));
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
        });
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
        });
    });
}


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
        .then(data => resolve(data));
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
        .then(data => resolve(data));
    });
}
