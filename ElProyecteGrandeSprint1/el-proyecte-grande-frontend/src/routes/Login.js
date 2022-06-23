import PropTypes from "prop-types";
import UserForm from "../components/UserForm";
import { useAppContext } from "../lib/contextLib";


const Login = ({postData, getData}) => {


const { userHasAuthenticated } = useAppContext();
let User;
  const SendDataToBackEnd = async (inputs) => {
      let validateResponse = await postData("Validate", inputs);
      console.log(validateResponse);
      if (validateResponse === "True") {
          User = await getData(`name/${inputs["username"]}`)
          userHasAuthenticated(true);
          console.log(User);
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