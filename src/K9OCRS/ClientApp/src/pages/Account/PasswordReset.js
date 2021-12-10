import React, { useState } from "react";
import { Row, Col, Button, Input } from "reactstrap";
//import axios from "axios";

async function handleSubmit(email) {
  // const response = await axios.post("/api/passwordreset", {
  //   email,
  // });
}

function ValidateEmail(e) {
  //Validate email
  let regex = /[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+/i;
  if (regex.test(e.value)) {
    e.setAttribute("isvalid", "true");
    e.setCustomValidity("");
  } else {
    e.setCustomValidity("Not a valid email format.");
    e.reportValidity();
  }
}

function PasswordReset() {
  const [email, setEmail] = useState("");

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        handleSubmit(email);
      }}
    >
      <h1 className="d-flex justify-content-center mt-4 font-weight-bold">
        Password Reset
      </h1>
      <p className="d-flex justify-content-center mt-4 font-weight-bold">
        We will send you an email with the steps to reset your password.
      </p>

      <Row mt="3">
        <Col className="mx-auto" lg="3">
          <div className="input-group mb-3">
            <Input
              type="email"
              className="form-control"
              placeholder="Email Address"
              htmlFor="Email"
              name="email"
              value={email}
              onChange={(e) => {
                ValidateEmail(e.target);
                setEmail(e.target.value);
              }}
              required
            />
          </div>
        </Col>
      </Row>

      <Row>
        <Col className="mx-auto" lg="3">
          <Button
            className="mx-auto d-flex justify-content-center"
            size="lg"
            type="submit"
            color="primary"
          >
            Reset
          </Button>
        </Col>
      </Row>
    </form>
  );
}

export default PasswordReset;
