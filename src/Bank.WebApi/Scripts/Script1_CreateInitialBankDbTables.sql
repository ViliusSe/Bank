CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    address VARCHAR(50),
    CreatedAt TIMESTAMPTZ,
    UpdatedAt TIMESTAMPTZ,
    DeletedAt TIMESTAMPTZ
);

CREATE TABLE accounts (
    id SERIAL PRIMARY KEY,
    IBAN VARCHAR(30) CHECK (IBAN ~* '^[A-Z]{2}\d{2}-\d{4}-\d{4}-\d{4}-\d{4}-\d{4}$') NOT NULL,
    type VARCHAR(20) NOT NULL,
    balance MONEY NOT NULL,
    userId INTEGER REFERENCES users(id) ON DELETE CASCADE NOT NULL,
    CreatedAt TIMESTAMPTZ,
    UpdatedAt TIMESTAMPTZ,
    DeletedAt TIMESTAMPTZ
);

CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    debitorAccount INTEGER REFERENCES accounts(id) ON DELETE CASCADE NOT NULL,
    creditorAccount INTEGER REFERENCES accounts(id) ON DELETE CASCADE NOT NULL,
    amount NUMERIC CHECK (amount >= 0) NOT NULL,
    date TIMESTAMPTZ NOT NULL,
    CreatedAt TIMESTAMPTZ,
    UpdatedAt TIMESTAMPTZ,
    DeletedAt TIMESTAMPTZ
);