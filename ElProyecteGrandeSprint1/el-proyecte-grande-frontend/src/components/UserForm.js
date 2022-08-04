import React from 'react';
import { useState } from 'react';
import PropTypes from "prop-types";

const UserForm = ({ SendDataToBackEnd, Page }) => {

    const [inputs, setInputs] = useState({});

    const handleChange = (event) => {
        const UserName = event.target.name;
        const Password = event.target.value;
        setInputs(values => ({ ...values, [UserName]: Password }))
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        console.log(inputs);
        SendDataToBackEnd(inputs).then(data => {
            console.log(data);
            if (data === "false") {
                alert("Your username or password is incorrect")
            } else if (data === "True" || data === "Registered Successfully") {
                window.location.href = "/";
            }
            else {
                alert(data)
            }
        })
    }

    return (
        <div style={{ height: "100%", width: "100%" }}>
            <form className={'form-style-1'} onSubmit={handleSubmit}>
                <h3 style={{ textAlignLast: "center", padding: "1rem" }} >{Page}</h3>
                <label>Enter your name:
                    <br></br>
                    <input
                        className={"search-by-game-news-button btn-outline-light"}
                        type="text"
                        name="username"
                        value={inputs.username || ""}
                        onChange={handleChange}
                        required
                    />
                </label>
                <br></br>
                <label>Enter your password:
                    <br></br>
                    <input
                        className={"search-by-game-news-button btn-outline-light"}
                        type="password"
                        name="password"
                        value={inputs.password || ""}
                        onChange={handleChange}
                        required
                    />
                    <br></br>
                </label>
                <div style={{ textAlignLast: "center", padding: "2rem" }}>
                    <input className={"search-by-game-news-button btn-outline-light"} type="submit" />
                </div>
            </form>
        </div>
    )
}
UserForm.prototype = {
    postData: PropTypes.func,
    SendDataToBackEnd: PropTypes.func,
    Page: PropTypes.string
}


export default UserForm