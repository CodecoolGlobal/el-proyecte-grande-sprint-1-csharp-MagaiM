import PropTypes from "prop-types";
import UserForm from "../components/UserForm";
import { useCookies } from 'react-cookie';

const Login = ({postData, getData}) => {

  let User;
  const [cookies, setCookie] = useCookies(['user_id']);

  const SendDataToBackEnd = async (inputs) => {
      let validateResponse = await postData("Validate", inputs);
      console.log(validateResponse);
      if (validateResponse === "True") {
          User = await getData(`name/${inputs["username"]}`)
          setCookie('user_id', User["id"], { path: '/' });
      }
    }

  return (
    <UserForm postData={postData} SendDataToBackEnd={SendDataToBackEnd}></UserForm>
  )
}

Login.prototype = {
    postData: PropTypes.func,
    getData: PropTypes.func
}

export default Login