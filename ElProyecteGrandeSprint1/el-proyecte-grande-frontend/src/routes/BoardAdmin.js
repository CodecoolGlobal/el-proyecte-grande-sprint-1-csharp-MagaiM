import React, {useState, useEffect} from "react";
import UserService from "../services/user.service";
import ProfileDetailsWithSmallNames from "../components/ProfileDetailsWithSmallNames";

const BoardAdmin = () => {
    const [content, setContent] = useState([]);
    const [currentProfile, setcurrentProfile] = useState(undefined);

    useEffect(() => {
        UserService.getAdminBoard().then(
            (response) => {
                setContent(response.data);
            },
            (error) => {
                const _content =
                    (error.response &&
                        error.response.data &&
                        error.response.data.message) ||
                    error.message ||
                    error.toString();
                setContent(_content);
            }
        );
    }, []);


    const openProfile = (e) => {
        content.forEach(element => {
            if (element.id == e.target.dataset.id) {
                setcurrentProfile(element);
            }
        });
    };


    const closeProfile = () =>{
        setcurrentProfile(undefined);
    }

    if (currentProfile) return (<ProfileDetailsWithSmallNames currentUser={currentProfile} closeProfile={closeProfile}/>);
    return (
        <div className="container">
            <h1><strong style={{color: "rgba(255,255,255,.55)"}}>Users</strong></h1>
            {content.map((item, index) => {
                return (
                    <div key={index} className="other-news-card">
                        <a className='card-title' onClick={openProfile}><h5 className="card-title" data-id={item.id}>{item.userName}</h5></a>
                    </div>
                )
            })}
        </div>
    );
};

export default BoardAdmin;