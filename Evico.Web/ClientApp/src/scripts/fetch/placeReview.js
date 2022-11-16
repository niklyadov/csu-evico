import { PlaceReview } from "../../components/classes/PlaceReview";
import config from "../../config";
import { errorHanlde } from "./errors";

const token = config.bearerToken;

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
        .catch(errorHanlde);
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
        .catch(errorHanlde);
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
        .catch(errorHanlde);
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
        .catch(errorHanlde);
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
        .catch(errorHanlde);
    });
}