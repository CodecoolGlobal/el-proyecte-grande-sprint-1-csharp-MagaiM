import PropTypes from "prop-types";
import UserForm from "../components/UserForm";
import { useCookies } from 'react-cookie';

const Login = ({postData, getData}) => {

  let User;
  const [cookies, setCookie] = useCookies(['user_id']);
  

  const SendDataToBackEnd = async (inputs) => {
      let validateResponse = await postData("Validate", inputs);
      if (validateResponse === "True") {
        User = await getData(`name/${inputs["username"]}`)
        setCookie('user_id', User["id"], { path: '/' });
        localStorage.setItem('user_id', User["id"]);
        localStorage.setItem('user_name', User["userName"]);
        localStorage.setItem('user_reputation', User["reputation"]);
        localStorage.setItem('user_rank', User["rank"]);
      }else{
          return validateResponse;
      }
    }

  return (
    <UserForm postData={postData} SendDataToBackEnd={SendDataToBackEnd} Page={"Login"}></UserForm>
  )
}

Login.prototype = {
    postData: PropTypes.func,
    getData: PropTypes.func
}

export default Login