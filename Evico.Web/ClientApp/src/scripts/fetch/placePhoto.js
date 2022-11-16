import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImY1YmJjYmM5LWJlYjctNDBlZS05YzdjLTE3YzY2Mjg3OWVmOCIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiMjhiNzBjNTgtYWQxNy00OWNlLWJmOTEtOGVhM2JkZDFjY2UzIiwibmJmIjoxNjY4NDEzNTY3LCJleHAiOjE2NjkwMTgzNjcsImlhdCI6MTY2ODQxMzU2NywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.pPV-ojMNwAIyo4SB3tlja002xYvMV4JUEInhwwagHhmu_e1TxBeSeGlvxxn_dk0UQoNHNn6B9BnSm_22GstQOA";

export const createPlacePhoto = function (placeId, photo) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}/photo`, {
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
        .then(data => resolve(data));
    });
}


export const deletePlacePhotoById = function (placeId, photoId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}place/${placeId}/photo/${photoId}`, {
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