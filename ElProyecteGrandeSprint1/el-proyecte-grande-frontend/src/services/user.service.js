import axios from "axios";
import authHeader from "./auth-header";

//const API_URL = "http://localhost:8080/api/test/";
const API_URL = "https://localhost:44321/";

const getPublicContent = () => {
  return axios.get(API_URL + "all");
};

const getArticleBoard = () => {
  return axios.get(API_URL + "articles", {headers: authHeader() });
};

const getUserBoard = () => {
  return axios.get(API_URL + "user", { headers: authHeader() });
};

const getModeratorBoard = () => {
  return axios.get(API_URL + "articles", { headers: authHeader() });
};

const getAdminBoard = () => {
  return axios.get(API_URL + "admin", { headers: authHeader() });
};

const UserService = {
  getPublicContent,
  getUserBoard,
  getModeratorBoard,
  getAdminBoard,
  getArticleBoard,
};

export default UserService;