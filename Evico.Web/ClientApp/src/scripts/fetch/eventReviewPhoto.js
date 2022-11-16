import config from "../../config";
import { errorHanlde } from "./errors";

const token = config.bearerToken;

export const createEventReviewPhoto = function (eventId, reviewId, photo) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}/review/${reviewId}/photo`, {
            method: "POST",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`,
                "Content-Type": "multipart/form-data"
            },
            body: photo
        })
        .then(response => response.json())
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const deleteEventReviewPhotoById = function (eventId, reviewId, photoId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}/review/${reviewId}/photo/${photoId}`, {
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