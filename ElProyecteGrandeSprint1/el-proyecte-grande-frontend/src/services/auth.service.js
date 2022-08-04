import axios from "axios";

//const API_URL = "http://localhost:8080/api/auth/";
const API_URL = "https://el-proyecte-grande-kvm-gaming.herokuapp.com/";
const register = (username, email, password) => {
  return axios.post(API_URL + "register", {
    username,
    email,
    password,
  });
};

const login = (username, password) => {
  return axios
    .post(API_URL + "login", {
      username,
      password,
    })
    .then((response) => {
      if (response.data.AccessToken) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }
      return response.data;
    });
};

const validateEmail = (email) => {
  return axios.post(API_URL + `${email}`, {
    email
  })
  .then((response) => {
    return response.data
  })
}

const ChangePassword = (password, email, username, emailId) => {
  return axios.post(API_URL + `password/` + `${emailId}`, {
    username,
    email,
    password
  })
  .then((response) => {
    return response.data
  })
}

const ForgotPassword = (username, email, password) => {

  return axios.post(API_URL + "send", {
    username,
    email,
    password
  })
}
const logout = () => {
  localStorage.removeItem("user");
};

const getCurrentUser = () => {
  return JSON.parse(localStorage.getItem("user"));
};

const AuthService = {
  register,
  login,
  logout,
  getCurrentUser,
  validateEmail,
  ForgotPassword,
  ChangePassword,
};

export default AuthService;