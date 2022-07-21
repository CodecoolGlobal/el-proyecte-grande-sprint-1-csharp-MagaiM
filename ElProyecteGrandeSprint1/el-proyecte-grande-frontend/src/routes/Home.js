import React, {useEffect, useState} from 'react';
import {wait} from "@testing-library/user-event/dist/utils";
import PropTypes from 'prop-types';
import AuthService from '../services/auth.service';

const Home = ({fetchData})  => {
    const currentUser = AuthService.getCurrentUser();
    const [homeData, setHomeData] = useState([]);
    const [slideData, setSlideData] = useState([]);
    
    let slideIndex = 0;
    let timeout = '';

    useEffect(() => {
        fetchData('https://localhost:44321/Home')
            .then(data => {
                data = data.slice(0, 15);
                setHomeData(data);
        });
    
    }, [])

    useEffect(() => {
        if (currentUser) {
        fetchData('https://localhost:44321/Deals?sortBy=Deal Rating&pageSize=20')
            .then(data => {
            data = data.slice(0, 15);
            setSlideData(data);
        });
        wait(2000).then(x => { showSlides() });
    }}, [])
    
    function showSlides() {
        try {
            if(timeout)
                stopTimeout(timeout);
            let i;
            let slides = document.getElementsByClassName("mySlides");
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            slideIndex++;
            if (slideIndex > slides.length) { slideIndex = 1 }
            slides[slideIndex - 1].style.display = "block";
            timeout = setTimeout(showSlides, 5000); // Change image every 5 seconds
        } catch {}
    }
    
    function stopTimeout(timeout) {
        clearTimeout(timeout);
    }

    return (
            <div>
                <div className="slideshow-container" id="slideshow-container" style={{textAlignLast: 'center'}}>
                    {slideData.map((data, index) => (
                        <div key={index} className="mySlides fade" style={{ display: 'none'}}>
                            <img className="slide-show-image" src={data.Image} alt={'Image'}/>
                                <h5 className="card-title text-center">{data.Title}</h5>
                                <p className="card-text"><strong> Store name: {data.StoreName} <br></br> Sale price:
                                    ${String(data.SalePrice)} <br></br> Normal price: $ {String(data.NormalPrice)}</strong></p>
                        </div>
                    ))}

                </div>
                <br></br>
                <br></br>
                <div id='containerbody' style={{textAlign: 'center'}}>
                    {homeData.map((data, index) => (
                        <div key={index} className="col-md-auto col-md-auto bg-dark"
                             style={{display: 'inline-block', width: '300px', height: '475px', marginTop: '10px', marginBottom: '10px'}}>
                            <div className="card" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 15px 20px 20px',height: '465px'}}>
                                <a href={data.GameUrl}><img src={data.Thumbnail} alt={"Thumbnail"}
                                                            style={{width: '100%'}}/></a>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F'}}>
                                    <h5 className="card-title text-center">{data.Title}</h5>
                                    <p className="card-text text-box">Short Description: {data.ShortDescription}</p>
                                    <p className="card-text">Genre: {data.Genre}</p>
                                    <p className="card-text">Platform: {data.Platform}</p>
                                    <p className="card-text text-center">
                                        <strong>ReleaseDate: {data.ReleaseDate}</strong></p>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
    );
}

Home.propTypes ={
    fetchData: PropTypes.func
  };

export default Home;