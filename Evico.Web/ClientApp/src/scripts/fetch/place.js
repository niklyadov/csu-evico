import { Place } from "../../components/classes/Place";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjUyZWVlYWNkLWNiOTMtNGE4Yy05ZjFiLTRmNTBlNTVjZmU3MyIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiOWMzNjZjNTctZTlhNC00ODg0LTkwMDUtYWY5MDAwNDE4Mzc5IiwibmJmIjoxNjY4MTU2MzM3LCJleHAiOjE2Njg3NjExMzcsImlhdCI6MTY2ODE1NjMzNywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.VEAw-QDPRCIQcdqVnRIXmyXrlfm_RE4EYxw-X2dVeomNL7EBDAV5Kn7SzfpZOkDat9Ho1uHfdNkSGm06ZXq_4w";

const placeRecord = {"locationLatitude": 0, "locationLongitude": 0, "name": "string", "description": "string"};
const changedPlaceRecord = {"id": 1, "locationLatitude": 1, "locationLongitude": 1, "name": "string123", "description": "string123"};

const placeReview = {"comment":"Comment21312313123123123123", "rate":1};
const changedPlaceReview = {"id": 1,"comment":"12321323123Comment", "rate":2};

// post
export const createPlace = function () {
    fetch(`${config.api}place`, {
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
    .then(x => console.log(x));

}
// get list
export const getPlacesList = function () {
    let placesList = [];
    fetch(`${config.api}place`, {
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
            console.log(placeObj);
        }
    });
}
// get by id
export const getPlaceById = function (placeId) {
    fetch(`${config.api}place/${placeId}`, {
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
        console.log(placeObj);
    });
}
// put
export const changePlace = function () {
    fetch(`${config.api}place`, {
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
    .then(x => console.log(x));
}
// delete
export const deletePlaceById = function (placeId) {
    fetch(`${config.api}place/${placeId}`, {
        method: "DELETE",
        mode: "cors",
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`
        }
    })
    .then(response => response.json())
    .then(x => console.log(x));
}


// post review
export const createPlaceReview = function (placeId) {
    fetch(`${config.api}place/${placeId}/review`, {
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
    .then(x => console.log(x));
}
// get reviews list by place id
export const getReviewsByPlaceId = function (placeId) {
    fetch(`${config.api}place/${placeId}/review`, { 
        method: "GET",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`
          }
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
        for (const review of data){
            // Todo new PlaceReview(review);
        }
    });
}
// get review by id
export const getReviewByIdByPlaceId = function (placeId, reviewId) {
    fetch(`${config.api}place/${placeId}/review/${reviewId}`, { 
        method: "GET",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`
          }
    })
    .then(response => response.json())
    .then(x => console.log(x));
}
// put review
export const changePlaceReview = function (placeId) {
    fetch(`${config.api}place/${placeId}/review`, {
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
    .then(x => console.log(x));
}
// delete review
export const deleteReviewById = function (placeId, reviewId) {
    fetch(`${config.api}place/${placeId}/review/${reviewId}`, { 
        method: "DELETE",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`
          }
    })
    .then(response => response.json())
    .then(x => console.log(x));
}