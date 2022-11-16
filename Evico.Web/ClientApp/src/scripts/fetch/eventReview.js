import { EventReview } from "../../components/classes/EventReview";
import config from "../../config";
import { errorHanlde } from "./errors";

const token = config.bearerToken;

/**
 * 
 * @param {EventReview} eventReview 
 */
export const createEventReview = function (eventReview) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventReview.eventId}/review`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(eventReview)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const getReviewsByEventId = function (eventId) {
    return new Promise(async (resolve, reject) => {
        let reviewsList = [];
        return fetch(`${config.api}event/${eventId}/review`, { 
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
                let reviewObj = new EventReview(review);
                reviewsList.push(reviewObj);
            }
            resolve(reviewsList);
        })
        .catch(errorHanlde);
    });
}


export const getReviewByIdByEventId = function (eventId, reviewId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}/review/${reviewId}`, { 
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let reviewObj = new EventReview(data);
            resolve(reviewObj);
        })
        .catch(errorHanlde);
    });

}

/**
 * 
 * @param {EventReview} changedEventReview 
 */
export const changeEventReview = function (changedEventReview) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${changedEventReview.eventId}/review`, {
            method: "PUT",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify(changedEventReview)
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const deleteReviewById = function (eventId, reviewId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}/review/${reviewId}`, { 
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
