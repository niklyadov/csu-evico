import { EventReview } from "../../components/classes/EventReview";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImY1YmJjYmM5LWJlYjctNDBlZS05YzdjLTE3YzY2Mjg3OWVmOCIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiMjhiNzBjNTgtYWQxNy00OWNlLWJmOTEtOGVhM2JkZDFjY2UzIiwibmJmIjoxNjY4NDEzNTY3LCJleHAiOjE2NjkwMTgzNjcsImlhdCI6MTY2ODQxMzU2NywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.pPV-ojMNwAIyo4SB3tlja002xYvMV4JUEInhwwagHhmu_e1TxBeSeGlvxxn_dk0UQoNHNn6B9BnSm_22GstQOA";

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
        .then(data => resolve(data));
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
        });
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
        });
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
        .then(data => resolve(data));
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
        .then(data => resolve(data));
    });
}
