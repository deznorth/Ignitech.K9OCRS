import axios from "axios";
import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { ClassificationType } from "typescript";


const Confirm = () => {

  const { sectionId } = useParams();
  const [sectionDetail, setSectionDetail] = useState([]);
  const [loading, setLoading] = useState(true);
  const [alerts, setAlerts] = useState([]);

  useEffect(() => {
      async function getTest() {
        if (sectionId) {
          try {
            const res = await axios.get(`/api/ClassSections/ ${sectionId}`);
            setSectionDetail(res?.data);
            setLoading(false);
          } catch(err) {
            setLoading(false);
            setAlerts([{ color: 'danger', message: 'We\'re having issues getting the details for this class' }]);
          }
        }
      }
      getTest();
    }, [sectionId]);
    
  return (
    <div className="container">
      <h3>Thank you for choosing {sectionDetail.title}</h3>
      <div className="mt-3 mb-3">
        <div className="card">
          <div className="card-header">
            <h3>STAR Puppy</h3>
          </div>
          <div className="card-body">
            Raising a puppy can be fun and very rewarding and also a big
            challenge! The K-9 Obedience Club offers classes to help you train
            your puppy to become a self-confident, happy and easy-to-live-with
            companion.
          </div>
          <div className="card-footer">
            <span className="text-center font-weight-bold">
              Section: SP01 | Day: Monday | Time: 6PM | Start: 12/20/21 |
              Duration: 7 weeks | Instructor: Rebecca Grinsell | Seats: 5
            </span>
          </div>
        </div>
      </div>
      <h4>Please confirm your information:</h4>
      <ul>
        <li>
          <span className="font-weight-bold">Student Name: </span>"current
          user's name"
        </li>
        <li>
          <span className="font-weight-bold">Email:</span> "current user email"
        </li>
        <li>
          <span className="font-weight-bold">Phone:</span> "current user phone"
        </li>
      </ul>
      <p>Or update below:</p>
      <form className="mt-3">
        <label className="form-control-label">
          <span className="font-weight-bold">Student Name: </span>"current
          user's name"
        </label>
        <input
          className="form-control"
          type="text"
          id="sName"
          placeholder="Student Name"
        />
        <label className="form-control-label">
          <span className="font-weight-bold">Email:</span> "current user email"
        </label>
        <input
          className="form-control"
          type="email"
          id="email"
          placeholder="Email"
        />
        <label className="form-control-label">
          <span className="font-weight-bold">Phone:</span> "current user phone"
        </label>
        <input
          className="form-control"
          type="tel"
          id="phone"
          placeholder="Student Phone"
        />
      </form>
    </div>
  );
};

export default Confirm;
