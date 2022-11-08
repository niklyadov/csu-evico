import config from "../../config"

const token = "token";


class EventRecord{
    constructor(id, placeId, start, end, name, description){
        this.id = id;
        this.placeId = placeId;
        this.start = start;
        this.end = end;
        this.name = name;
        this.description = description;
    }
}

const eventRecord = new EventRecord(0,1,"2022-11-07T10:59:09.875Z","2022-11-07T10:59:09.875Z","Text","Description");
const changedEventRecord = new EventRecord(0,1,"2022-11-07T10:59:09.875Z","2022-11-07T10:59:09.875Z","New Text","New Description");

class EventReview{
    constructor(comment, rate){
        this.comment = comment;
        this.rate = rate;
    }
}
const eventReview = new EventReview("Comment21312313123123123123",1);
const changedEventReview = {"id": 1,"comment":"12321323123Comment", "rate":2};
// Post create
export const createEvent = function () {
    fetch(`${config.api}event`, {
        method: "POST",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
          },
        body: JSON.stringify(eventRecord)
    })
    .then(response => response.json())
    .then(x => console.log(x));

}

// Get list
export const getEventsList = function () {
    fetch(`${config.api}event`, {
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

// Get by id
export const getEventById = function (eventId) {
    fetch(`${config.api}event/${eventId}`, {
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

// Put
export const changeEvent = function () {
    fetch(`${config.api}event`, {
        method: "PUT",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
          },
        body: JSON.stringify(changedEventRecord)
    })
    .then(response => response.json())
    .then(x => console.log(x));
}

// delete
export const deleteEventById = function (eventId) {
    fetch(`${config.api}event/${eventId}`, {
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



// Post create review
export const createEventReview = function (eventId) {
    fetch(`${config.api}event/${eventId}/review`, {
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
    .then(x => console.log(x));
}


// Get reviews list by event id
export const getReviewsByEventId = function (eventId) {
    fetch(`${config.api}event/${eventId}/review`, { 
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

// Get one review by id
export const getReviewByIdByEventId = function (eventId, reviewId) {
    fetch(`${config.api}event/${eventId}/review/${reviewId}`, { 
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

// put
export const changeEventReview = function (eventId) {
    fetch(`${config.api}event/${eventId}/review`, {
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
    .then(x => console.log(x));
}
// delete
export const deleteReviewById = function (eventId, reviewId) {
    fetch(`${config.api}event/${eventId}/review/${reviewId}`, { 
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
