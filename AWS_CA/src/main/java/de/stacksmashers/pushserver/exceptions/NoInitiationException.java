package de.stacksmashers.pushserver.exceptions;


/**
 * Exception for the case that pushes are made before the server was initiated by the UIC
 */
public class NoInitiationException extends Exception {
    public NoInitiationException() {
    }

    public NoInitiationException(String message) {
        super(message);
    }
}
