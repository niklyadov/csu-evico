import { PlaceReview } from "../../components/classes/PlaceReview";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjUyZWVlYWNkLWNiOTMtNGE4Yy05ZjFiLTRmNTBlNTVjZmU3MyIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiOWMzNjZjNTctZTlhNC00ODg0LTkwMDUtYWY5MDAwNDE4Mzc5IiwibmJmIjoxNjY4MTU2MzM3LCJleHAiOjE2Njg3NjExMzcsImlhdCI6MTY2ODE1NjMzNywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.VEAw-QDPRCIQcdqVnRIXmyXrlfm_RE4EYxw-X2dVeomNL7EBDAV5Kn7SzfpZOkDat9Ho1uHfdNkSGm06ZXq_4w";

/**
 * 
 * @param {PlaceReview} placeReview 
 */
export const createPlaceReview = function (placeReview) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeReview.placeId}/review`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(placeReview)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(error => reject(error));
    });
}


export const getReviewsByPlaceId = function (placeId) {
    return new Promise(async (resolve, reject) => {
        let reviewsList = [];
        return fetch(`${config.api}place/${placeId}/review`, { 
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            for (const review of data){
                let reviewObj = new PlaceReview(review);
                reviewsList.push(reviewObj);
            }
            resolve(reviewsList);
        })
        .catch(error => reject(error));
    });
}


export const getReviewByIdByPlaceId = function (placeId, reviewId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}/review/${reviewId}`, { 
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let reviewObj = new PlaceReview(data);
            resolve(reviewObj);
        })
        .catch(error => reject(error));
    });
}



/**
 * 
 * @param {PlaceReview} changedPlaceReview 
 */
export const changePlaceReview = function (changedPlaceReview) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${changedPlaceReview.placeId}/review`, {
            method: "PUT",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(changedPlaceReview)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(error => reject(error));
    });
}


export const deleteReviewById = function (placeId, reviewId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}/review/${reviewId}`, { 
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