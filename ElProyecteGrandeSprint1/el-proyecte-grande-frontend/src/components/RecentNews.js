import React from 'react'
import RecentNewsCard from './RecentNewsCard'

const RecentNews = ({newsData}) => {
    return (
        <div className="recent-news">
            <h3 style={{color: '#c9b18e'}}>Recent news</h3><br/>
            {newsData.map((news, index) => (
                <div key={index} className="col-lg-3 col-lg-3" style={{display: 'inline-block', width: '100%'}}>
                    <RecentNewsCard news={news}/>
                    <br/>
                </div>
            ))}
        </div>
    )
}

export default RecentNews