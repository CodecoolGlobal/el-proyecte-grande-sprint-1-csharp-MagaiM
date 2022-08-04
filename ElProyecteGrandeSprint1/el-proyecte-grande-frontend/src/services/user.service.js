import axios from "axios";
import authHeader from "./auth-header";

//const API_URL = "http://localhost:8080/api/test/";
const API_URL = "https://el-proyecte-grande-kvm-gaming.herokuapp.com/";

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
  return axios.get(API_URL + "admin/users", {headers: authHeader()});
};

const ChangeArticle = (Title, Description, Theme, ArticleText, Id, author) => {
  return axios.put(API_URL + "change/" + `${Id}`,  { Title, Description, author ,Theme, ArticleText}, {headers: authHeader()} );
};

const UserService = {
  getPublicContent,
  getUserBoard,
  getModeratorBoard,
  getAdminBoard,
  getArticleBoard,
  ChangeArticle,
};

export default UserService;