import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5253';

// טיפול בשגיאות
axios.interceptors.response.use(
  response => response,
  error => {
    console.log(error);
    return Promise.reject(error);
  }
);

export default {

  getTasks: async () => {
    console.log('getTasks')
    return await (await axios.get(`/items`)).data;
  },

  addTask: async (name) => {
    console.log('addTask', name)
    return await (await axios.post(`/items`, { name: name })).data;
  },

  setCompleted: async (id, isco) => {
    console.log('setCompleted', { id, isco })
    return await (await axios.put(`/items/${id}/${isco}`)).data;
},

  deleteTask: async (id) => {
    console.log('deleteTask')
    return await (await axios.delete(`/items/${id}`, id)).data;
  }
};
