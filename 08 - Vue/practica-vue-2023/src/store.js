import { createStore } from 'vuex';

const store = createStore({
  state: {
    message: "Hello World with Vuex!"
  },
  mutations: {
    setMessage(state, newMessage) {
      state.message = newMessage;
    }
  }
});

export default store;
