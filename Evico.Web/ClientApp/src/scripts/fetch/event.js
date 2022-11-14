import { Event } from "../../components/classes/Event";
import config from "../../config"

const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6ImY1YmJjYmM5LWJlYjctNDBlZS05YzdjLTE3YzY2Mjg3OWVmOCIsInN1YiI6IjEiLCJuYW1lIjoiTmlraXRhX2hvdGRvZyIsImVtYWlsIjoiIiwianRpIjoiMjhiNzBjNTgtYWQxNy00OWNlLWJmOTEtOGVhM2JkZDFjY2UzIiwibmJmIjoxNjY4NDEzNTY3LCJleHAiOjE2NjkwMTgzNjcsImlhdCI6MTY2ODQxMzU2NywiaXNzIjoiQ1NVLUVWSUNPIiwiYXVkIjoiQ1NVLUVWSUNPIn0.pPV-ojMNwAIyo4SB3tlja002xYvMV4JUEInhwwagHhmu_e1TxBeSeGlvxxn_dk0UQoNHNn6B9BnSm_22GstQOA";

export const createEvent = function (eventRecord) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event`, {
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
        .then(data => resolve(data));
    });
}


export const getEventsList = function () {
    return new Promise(async (resolve, reject) => {
        let eventsList = [];
        return fetch(`${config.api}event`, {
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
            for (const event of data){
                let eventObj = new Event(event);
                eventsList.push(eventObj);
            }
            resolve(eventsList);
        });
    });
}


export const getEventById = function (eventId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}`, {
            method: "GET",
            mode: 'cors',
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let eventRecord = new Event(data);
            resolve(eventRecord);
        });
    });
}


export const changeEvent = function (changedEventRecord) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event`, {
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
        .then(data => resolve(data));
    });
}


export const deleteEventById = function (eventId) {
    return new Promise(async (resolve, reject) => {
        return fetch(`${config.api}event/${eventId}`, {
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
