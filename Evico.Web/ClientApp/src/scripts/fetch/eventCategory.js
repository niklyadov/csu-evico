import config from "../../config"

const token = "token";

const eventCategory = {
    "name": "name",
    "description": "desc",
}

// post
export const createEventCategory = function () {
    fetch (`${config.api}eventcategory`, {
        method: "POST",
        mode: 'cors',
        headers: {
            "accept": "text/plain",
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json"
        },
        body: JSON.stringify(eventCategory)
    })
    .then(response => response.json())
    .then(x => console.log(x));
}


// get list
export const getEventCategoriesList = function () {
    fetch(`${config.api}eventcategory`, {
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

// get by id

// put

// delete