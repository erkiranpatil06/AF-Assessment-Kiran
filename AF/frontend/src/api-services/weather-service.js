import Axios from 'axios';

const RESOURCE_NAME = '/Weather/forecast?city=';

export default {
    getForecast(name) {
        return Axios.get(`${RESOURCE_NAME}${name}`);
    }
};