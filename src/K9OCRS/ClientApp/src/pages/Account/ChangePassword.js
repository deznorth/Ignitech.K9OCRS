import React, { useState } from 'react';
import { Row, Col, Button, Input } from 'reactstrap';
import { useHistory } from 'react-router-dom';
import * as accountsApi from '../../util/apiClients/userAccounts';

function ValidatePassword(e) {
  //Validate password requirements
  let regex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$/i;
  if (regex.test(e.value)) {
    e.setAttribute('isvalid', 'true');
    e.setCustomValidity('');
  } else {
    e.setCustomValidity(
      'Password requires at least 8 characters, should contain at least one upper case, lower case, and a digit'
    );
    e.reportValidity();
  }
}

function ValidateConfirm(e, password) {
  //Validate passwords are the same
  if (password === e.value) {
    e.setAttribute('isvalid', 'true');
    e.setCustomValidity('');
  } else {
    e.setCustomValidity('Password does not match');
    e.reportValidity();
  }
}

function ChangePassword() {
  const queryParams = new URLSearchParams(window.location.search);
  const token = queryParams.get('token') + '';
  let history = useHistory();

  const [password, setPassword] = useState('');
  const [confirm, setConfirm] = useState('');
  const [result, setResult] = useState('');

  async function handleSubmit(token, password, history) {
    try {
      const response = await accountsApi.changePassword({ token, password });
      setResult(response);
      // loginAction({ email, password });
      history.push('/');
    } catch (err) {
      setResult(err.response);
    }
  }

  return (
    <form
      onSubmit={(e) => {
        e.preventDefault();
        handleSubmit(token, password, history);
      }}
    >
      <h1 className='d-flex justify-content-center mt-4 mb-4 font-weight-bold'>
        Change Password
      </h1>

      <Row>
        <Col lg='4' className='mx-auto'>
          <div className='input-group mb-3'>
            <Input
              type='password'
              className='form-control'
              placeholder='New Password'
              htmlFor='Password'
              name='password'
              value={password}
              onChange={(e) => {
                ValidatePassword(e.target);
                setPassword(e.target.value);
              }}
              required
            />
          </div>
        </Col>
      </Row>

      <Row>
        <Col lg='4' className='mx-auto'>
          <div className='input-group mb-3'>
            <Input
              type='password'
              className='form-control'
              placeholder='Confirm New Password'
              htmlFor='Confirm'
              name='confirm'
              value={confirm}
              onChange={(e) => {
                ValidateConfirm(e.target, password);
                setConfirm(e.target.value);
              }}
              required
            />
          </div>
        </Col>
      </Row>

      <Row>
        <Col className='mx-auto' lg='3'>
          <p className='text-danger d-flex justify-content-center mb-3'>
            {result?.data}
          </p>
          <Button
            className='mx-auto d-flex justify-content-center'
            size='lg'
            type='submit'
            color='primary'
          >
            Change
          </Button>
        </Col>
      </Row>
    </form>
  );
}

export default ChangePassword;