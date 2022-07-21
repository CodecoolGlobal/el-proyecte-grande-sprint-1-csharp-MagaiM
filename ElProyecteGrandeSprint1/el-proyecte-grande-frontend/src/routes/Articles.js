import React, { useState, useEffect } from "react";
import UserService from "../services/user.service";
import ArticleDesc from "../components/ArticleDesc";
import Article from "../components/Article";

const Articles = () => {
    const [content, setContent] = useState([]);
    const [currentArticle, setCurrentArticle] = useState(undefined);

    useEffect(() => {
    UserService.getArticleBoard().then(
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

    const openArticle = (e) => {
        content.forEach(element => {
            if (element.id == e.target.dataset.articleId) {
                setCurrentArticle(element);
            }
        });
    };

    const closeArticle = () =>{
        setCurrentArticle(undefined);
    }
    console.log(content);
    if (content === "Unauthorized") window.location.href = '/login';
    if (currentArticle) return (<Article item={currentArticle} closeArticle={closeArticle}/>);
    return (
        <div className="recent-news article-container">
            <h3 className='recent-news-title'>Articles</h3><br/>
            {content.map((item, index) => {
                return(
                <div key={index} className="other-news-card">
                    <ArticleDesc item={item} openArticle={openArticle} />
                </div>
            )})}
        </div>
        )
}

export default Articles