import { Event } from "../../components/classes/Event";
import config from "../../config";
import { errorHanlde } from "./errors";

const token = config.bearerToken;

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
        .then(data => resolve(data))
        .catch(errorHanlde);
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
        })
        .catch(errorHanlde);
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
        })
        .catch(errorHanlde);
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
        .then(data => resolve(data))
        .catch(errorHanlde);
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
        .then(data => resolve(data))
        .catch(errorHanlde);
    });
}


export const getUserEventsList = function () {
    return new Promise (async (resolve, reject) => {
        let eventsList = [];
        return fetch(`${config.api}event/my`, {
            method: "GET",
            mode: "cors",
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            for (const event of data){
                let eventObj = new Event(event);
                eventsList.push(eventObj);
            }
            resolve(eventsList);
        })
        .catch(errorHanlde);
    })
}


export const getUserEventById = function (eventId) {
    return new Promise (async (resolve, reject) => {
        return fetch(`${config.api}event/my/${eventId}`, {
            method: "GET",
            mode: "cors",
            headers: {
                "accept": "text/plain",
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => response.json())
        .then(data => {
            let eventObj = new Event(data);
            resolve(eventObj);
        })
        .catch(errorHanlde);
    })
}